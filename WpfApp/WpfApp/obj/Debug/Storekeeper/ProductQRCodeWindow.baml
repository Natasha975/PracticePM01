   M S B A M L   `   `   `  ����A  =WpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WpfApp.StorekeeperWpfApp.Storekeeper  5   	   ,    &WpfApp.Storekeeper.ProductQRCodeWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:WpfApp.Storekeeper     5   	   $	��470��5   	   $	��450��6   .��5	      � $��10q�6     � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $7��1Формирование QR кода товара��6   $��2��66   $��0��6J   $��17(�6W   $��Center=�6e   �� 5   
   $"��Выберите товар:��6   $��1��6*   $��0��67   $��5q�6G   �� 5   
   -   5         ��Name$ ProductComboBox��5      $��1��6,   $��1��69   $��5q�6I    �� DisplayMemberPath$ Название��6T   6q   �� 5   
   $��Название:��6   $��2��6$   $��0��61   $��5q�6A   �� 5    
   -   5         ��Name$ NameTextBox��5       $��2��6'   $��1��64   $��5q�6D    �� 
IsReadOnly . 6O   �� 5"   
   $��Артикул:��6   $��3��6#   $��0��60   $��5q�6@   �� 5#   
   -   5        $ ArticleTextBox��5#      $��3��6*   $��1��67   $��5q�6G    . 6R   �� 5%   
   $��Штрихкод:��6   $��4��6$   $��0��61   $��5q�6A   �� 5&   
   -   5        $ BarcodeTextBox��5&      $��4��6*   $��1��67   $��5q�6G    . 6R   �� 5(   
   $��2��6   $��5��6)    �� Orientation$ 
Horizontal=�66   $��Center=�6O   .+�5)      �� -   5         ��Name$ GenerateQRButton��5)      $*��$Сгенерировать QR-код��6/   $��5��6N   $��0��6[   $��2��6k   $��5q�6   $��Center=�6�   6�   �� 5*      -   5        $ ExportToWordButton��5*      $��Экспорт в Word��61   $��5��6J   $��0��6W   $��2��6g   $��5q�6{   $��Center=�6�   5+      �� 5-   
   -   5         ��Name$ QRCodeImage��5-      $��6��6%   $��0��62   $��2��6B   $��5q�6V   $	��200��6a   $	��200��6m   5.      5/      