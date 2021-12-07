# ThreadsEVM

Пример программы (сумма элементов массива):
```
0     input     n     3-0     n
1     input     n     4-0     n 
2     input     n     5-0     n
3     Merge     n     4-1     n
4     oper2     sum   6-0     n
5     Merge     n     7-0     n
6     branch    n     8-0     9-1
7     oper1     dec   10-1    n
8     oper1     true  10-0    n
9     TF        n     3-1     11-0
10    valve     n     12-0    n
11    output    n     n       n
12    branch    n     5-1     9-0
```
