# ПЗ21 — перегрузка методов класса

Консольное приложение Visual Studio по `PZ21_-_RP_ispolzuyuschego_peregruzku_metodov_klassa.pdf`.

## Запуск

Откройте `PZ21_Overloads.sln` в Visual Studio или выполните:

```text
dotnet run
```

Пункт 1 демонстрирует перегрузку `AverageMethods.Average(int[])` и `Average(float[])`: компилятор выбирает реализацию по типу массива. Пункт 2 показывает все четыре конструктора `Circle`, которые вызывают друг друга через `this(...)`:

1. `Circle()` — `X = 0`, `Y = 0`, `Radius = 0`;
2. `Circle(int radius)` — координаты нулевые;
3. `Circle(int x, int y)` — радиус нулевой;
4. `Circle(int x, int y, int radius)`.

Автопроверка:

```text
dotnet run -- --selftest
```
