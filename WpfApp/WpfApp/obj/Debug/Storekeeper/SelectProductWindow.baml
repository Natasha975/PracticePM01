   M S B A M L   `   `   `  ����A  =WpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WpfApp.StorekeeperWpfApp.Storekeeper  5   	   ,    &WpfApp.Storekeeper.SelectProductWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:WpfApp.Storekeeper     5   	   $	��300��5   	   $	��300��6   .��5	      �� $��10q�6   .+�5
   
   �� $��Выбор товара��6   $��17(�6'   $��Center=�65   �� 5   
   $"��Выберите склад:��6   �� 5   
   -   5          ��Name$  WarehouseComboBox��5       �� DisplayMemberPath$" НазваниеСклада��6.   $��0,0,0,10q�6Q   6c   �� 5   
   -   5         ��Name$ LabelIncome��5   6   $7��1Выберите товар (приходная):��6   $y�	Collapsed=�6K   �� 5   
   -   5        $  ProductComboBoxIncome��5      $ Название��62    �� SelectedValuePath$ 
Номер��6O   $y�	Collapsed=�6i   �� 5   
   -   5        $ LabelOutcome��5   6   $7��1Выберите товар (расходная):��6   $y�	Collapsed=�6L   �� 5   
   -   5        $  ProductComboBoxOutcome��5      $ 
Номер��63   $y�	Collapsed=�6M   ��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� ��6$   �� *Товар.Название+�� 5      $�� (кол. - ��6$   �� 5      ��6$   �� *Количество+�� 5      $��)��6$   5      5      5       �� 5/   
   $��Количество:��6   �� 50   
   -   5         ��Name$ QuantityTextBox��50      $��0,0,0,10q�6+   �� 52   
   -   5        $��Добавить��52      $	��100��6<   $
��Left=�6H   53      54      