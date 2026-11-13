# ПЗ25 — компоненты ввода и отображения чисел, дат и времени

Windows Forms приложение Visual Studio по `PZ25_-_Sozdanie_proekta_s_ispolzovaniem_komponentov_vvoda_i_otobrazhenia_chisel_dat_i_vremeni.pdf`.

## Запуск

Откройте `PZ25_DateTime.sln` в Visual Studio и нажмите F5 или выполните:

```text
dotnet run
```

Автопроверка:

```text
dotnet run -- --selftest
```

## Возможности

- текущие дата и время обновляются каждую секунду;
- два `DateTimePicker` с пользовательским форматом `dd.MM.yyyy HH:mm:ss`;
- `NumericUpDown` для количества дней;
- вывод всех стандартных форматов `D, d, F, f, G, g, M, O, R, s, T, t, U, u, Y` и собственных форматов;
- разность дат через `Subtract` с выводом полных дней, часов и минут;
- прибавление/вычитание дней через `AddDays`;
- расчёт возраста через `AddYears`/`AddMonths`;
- проверка високосного года и числа дней в месяце.
