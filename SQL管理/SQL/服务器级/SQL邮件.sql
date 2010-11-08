1. 启用 SQL 邮件功能。 

use master
go

exec sp_configure 'show advanced options',1
go

reconfigure
go

exec sp_configure 'Database mail XPs',1
go

reconfigure
go

2. 在 SQL Server 2005 中添加邮件帐户（account） 

exec msdb..sysmail_add_account_sp
        @account_name            = 'p.c.w.l'                 -- 邮件帐户名称（SQL Server 使用）
       ,@email_address           = 'webmaster@sqlstudy.com'  -- 发件人邮件地址
       ,@display_name            = null                      -- 发件人姓名
       ,@replyto_address         = null
       ,@description             = null
       ,@mailserver_name         = '58.215.64.159'           -- 邮件服务器地址
       ,@mailserver_type         = 'SMTP'                    -- 邮件协议（SQL 2005 只支持 SMTP）
       ,@port                    = 25                        -- 邮件服务器端口
       ,@username                = 'webmaster@sqlstudy.com'  -- 用户名
       ,@password                = 'xxxxxxxxx'               -- 密码
       ,@use_default_credentials = 0
       ,@enable_ssl              = 0
       ,@account_id              = null

3. 在 SQL Server 2005 中添加 profile 

exec msdb..sysmail_add_profile_sp @profile_name = 'dba_profile'      -- profile 名称 
                                 ,@description  = 'dba mail profile' -- profile 描述 
                                 ,@profile_id   = null

4. 在 SQL Server 2005 中映射 account 和 profile 

exec msdb..sysmail_add_profileaccount_sp  @profile_name    = 'dba_profile' -- profile 名称 
                                         ,@account_name    = 'p.c.w.l'     -- account 名称 
                                         ,@sequence_number = 1             -- account 在 profile 中顺序 

5. 利用 SQL Server 2005 Database Mail 功能发送邮件。 

exec msdb..sp_send_dbmail @profile_name =  'dba_profile'               -- profile 名称 
                         ,@recipients   =  'sqlstudy@163.com'          -- 收件人邮箱 
                         ,@subject      =  'SQL Server 2005 Mail Test' -- 邮件标题 
                         ,@body         =  'Hello Mail!'               -- 邮件内容 
                         ,@body_format  =  'TEXT'                      -- 邮件格式 

6. 查看邮件发送情况： 

use msdb
go

select * from sysmail_allitems
select * from sysmail_mailitems
select * from sysmail_event_log

如果不是以 sa 帐户发送邮件，则可能会出现错误： 

Msg 229, Level 14, State 5, Procedure sp_send_dbmail, Line 1
EXECUTE permission denied on object 'sp_send_dbmail', database 'msdb', schema 'dbo'.

这是因为，当前 SQL Server 登陆帐户（login），在 msdb 数据库中没有发送数据库邮件的权限，需要加入 msdb 数据库用户，并通过加入 sp_addrolemember 角色赋予权限。假设该SQL Server 登陆帐户名字为 “dba” 

use msdb
go

create user dba for login dba
go

exec dbo.sp_addrolemember @rolename   = 'DatabaseMailUserRole',
                          @membername = 'dba'
go

此时，再次发送数据库邮件，仍可能有错误： 

Msg 14607, Level 16, State 1, Procedure sp_send_dbmail, Line 119
profile name is not valid

虽然，数据库用户 “dba” 已经在 msdb 中拥有发送邮件的权限了，但这还不够，他还需要有使用 profile：“dba_profile” 的权限。 

use msdb
go

exec sysmail_add_principalprofile_sp  @principal_name = 'dba'
                                     ,@profile_name   = 'dba_profile'
                                     ,@is_default     = 1

从上面的参数 @is_default=1 可以看出，一个数据库用户可以在多个 mail profile 拥有发送权限 