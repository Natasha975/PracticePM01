   M S B A M L   `   `   `  ����A  =WpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WpfAppWpfApp  5   	       WpfApp.AdminWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:WpfApp     5   	     � Title$(  "Администрирование��5   	   $	��450��6#   $	��800��60   .��5	      � .+�5
   
   �� $��10q�6   .@�5      �� $��Склады��6   .��5      �� .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ СкладыDataGrid��5         AutoGenerateColumns .     CanUserAddRows .    
IsReadOnly .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Номер��61      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6@   �� *
Номер+6Z     5      $# Название склада��61    6J   �� *НазваниеСклада+  5      $ Субъект��61    6B   �� *Субъект+  5      $ 
Улица��61    6@   �� *
Улица+  5      $ Дом��61    6>   �� *Дом+4  .System.Windows.Controls.DataGridComboBoxColumn  5      $ Тип склада��65   	   DisplayMemberPath$	 Наименование��6s   
   SelectedValuePath$
 
Номер��6�      SelectedItemBinding 6I   �� *ТипСклада+   ElementStyle 5   "   �� 5   &    �� 
TargetType" ��6,   .�5   *   ��  �� Property � �� �� Value 6H   �� *$"DataContext.ТипыСкладов+ �� RelativeSource ��  �� AncestorType$ DataGridL�5   '      EditingElementStyle 5   "   �� 5   &   " ��6,   .�5   *   ��  � �� 6H   �� *$"DataContext.ТипыСкладов+ �� $ DataGridL�5   '   5        5       $ Зоны хранения��61    6H   �� *ЗоныХранения+5"      �� 5#       �� Orientation$ 
Horizontal=�6!   .+�5$      �� -   5        $��Сохранить��5$   !   $	��150��65   $��30��6A   $��5q�6k   5%      �� 5&      $ 
Horizontal=�6!   .+�5'      �� -   5        $��Удалить��5'   !   $	��150��63   $��30��6?   $��5q�6f   5(      5)      5*      �� 5+      $��Товары��6   .��5,      �� .+�5-        -   5        $ ТоварыDataGrid��5-       .   .  5.        5/      $ 
Номер��61    .  6@   �� *
Номер+6Z     50      $ Название��61    6C   �� *Название+  51      $ Артикул��61    6B   �� *Артикул+  52      $ Штрихкод��61    6C   �� *Штрихкод+  53      $ Категория��61    6D   �� *Категория+  54      $' !Единица измерения��61    6L   �� *" ЕдиницаИзмерения+  55      $" Цена за единицу��61    6J   �� *ЦенаЗаЕдиницу+  56      $+ %Минимальный остаток��61    6N   �� *&$МинимальныйОстаток+58      �� 59      $ 
Horizontal=�6!   .+�5:      �� -   5        $��Сохранить��5:   !   $	��150��65   $��30��6A   $��5q�6k   5;      �� 5<      $ 
Horizontal=�6!   .+�5=      �� -   5        $��Удалить��5=   !   $	��150��63   $��30��6?   $��5q�6f   5>      5?      5@      �� 5A      $��Клиенты��6   .��5B      �� .+�5C        -   5        $ КлиентыDataGrid��5C       .   .  5D        5E      $ 
Номер��61    .  6@   �� *
Номер+6Z     5F      $ Название��61    6C   �� *Название+  5G      $ Телефон��61    6B   �� *Tелефон+  5H      $ Email��61    6@   �� *Email+  5I      $ Субъект��61    6B   �� *Субъект+  5J      $ 
Улица��61    6@   �� *
Улица+  5K      $ Дом��61    6>   �� *Дом+5M      �� 5N      $ 
Horizontal=�6!   .+�5O      �� -   5        $��Сохранить��5O   !   $	��150��6T   $��30��6`   $��5q�6l   5P      �� 5Q      $ 
Horizontal=�6!   .+�5R      �� -	   5        $��Удалить��5R   !   $	��150��6P   $��30��6\   $��5q�6h   5S      5T      5U      �� 5V      $��Поставщики��6   .��5W      �� .+�5X        -
   5        $" ПоставщикиDataGrid��5X       .   .  5Y        5Z      $ 
Номер��61    .  6@   �� *
Номер+6Z     5[      $ Название��61    6C   �� *Название+  5\      $ ИНН/КПП��61    6B   �� *ИННКПП+  5]      $ Телефон��61    6B   �� *Tелефон+  5^      $ Email��61    6@   �� *Email+  5_      $ Субъект��61    6B   �� *Субъект+  5`      $ 
Улица��61    6@   �� *
Улица+  5a      $ Дом��61    6>   �� *Дом+5c      �� 5d      $ 
Horizontal=�6!   .+�5e      �� -   5        $��Сохранить��5e   !   $	��150��6W   $��30��6c   $��5q�6o   5f      �� 5g      $ 
Horizontal=�6!   .+�5h      �� -   5        $��Удалить��5h   !   $	��150��6S   $��30��6_   $��5q�6k   5i      5j      5k      �� 5l      $��Пользователи��6   .��5m      �� .+�5n        -   5        $&  ПользователиDataGrid��5n       .   .  5o        5p      $ 
Номер��61    .  6@   �� *
Номер+6Z     5q      $ Фамилия��61    6B   �� *Фамилия+  5r      $ Имя��61    6>   �� *Имя+  5s      $ Отчество��61    6C   �� *Отчество+  5t      $ Роль��61    6?   �� *
Роль+  5u      $ 
Логин��61    6@   �� *
Логин+  5v      $ Пароль��61    6A   �� *Пароль+5x      �� 5y      $ 
Horizontal=�6!   .+�5z      �� -   5        $��Сохранить��5z   !   $	��150��6Y   $��30��6e   $��5q�6q   5{      �� 5|      $ 
Horizontal=�6!   .+�5}      �� -   5        $��Удалить��5}   !   $	��150��6U   $��30��6a   $��5q�6m   5~      5      5�      5�      5�      5�      