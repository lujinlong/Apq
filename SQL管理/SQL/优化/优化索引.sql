
[聚集索引]
一般不使用,下列情况可使用
1.该表只需要一个索引.
2.该表不需要查询,或任何查询均无法抽象出索引,但需要数据行的物理存储按某种顺序.

[填充因子]
1.低更改的表（读写比率为100：1）：100％的填充因子
2.高更改的表（写超过读）：50-70％的填充因子
3.读写各一半的：80-90％的填充因子
