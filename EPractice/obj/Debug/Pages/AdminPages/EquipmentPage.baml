   M S B A M L   `   `   `  ����D  @EPractice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:EPractice.Pages.AdminPagesEPractice.Pages.AdminPages  5      .    (EPractice.Pages.AdminPages.EquipmentPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:EPractice.Pages.AdminPages     5        N� Title$  EquipmentPage��5	      .��5      � .+�5   
   �� $��1��6   $
��Auto=�6$   .��5      �� $��40,20,40,20q�6   .+�5      �� $��ИНВЕНТАРЬ��6   $��Center=�6,   $��24(�6I   $
��Bold"�6W   �� 5      -   5         ��Name$ TotalRunnersText��5      $W��QВсего зарегистрировано бегунов на марафон: 0��66   $
��Bold"�5      $��	0,10,0,20q�6&   $��Center=�69   &   System.Windows.Controls.DataGrid  5      -   5          Name$ KitSelectionGrid��5         AutoGenerateColumns .     
IsReadOnly .    HeadersVisibility$ Column=�5      $��1q�6.      GridLinesVisibility$
 None=�6B   $��0,0,0,20q�5         CanUserAddRows .     CanUserDeleteRows .  	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Комплект��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6[   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� *	KitName+6[     5      $
 Тип A��6-   $ * 6V    6<   �� *TypeA+6V     5      $
 Тип B��6-   $ * 6V    6<   �� *TypeB+6V     5      $
 Тип C��6-   $ * 6V    6<   �� *TypeC+6V     5      $
 Необходимо��6-   $ * 6[    6A   �� *Total+6[     5      $
 Остаток��6-   $ * 6\    6>   �� *	Remaining+6\   5      �� 5       $%��Состав комплекта��6   $
��Bold"�64   $��0,10,0,5q�6F     5!      -   5        $ InventoryItemsGrid��5!       .   . $ Column=�5"      $��1q�6.   $
 None=�6B    .   .  	 5$        5%      $
 
Номер��6-   $ * 6Y    6<   �� *
ItemName+6Y   4  .System.Windows.Controls.DataGridTemplateColumn  5&      $
 Тип A��61   $ * 6@      CellTemplate 5'      �� 5(   "   .R�5)   &   �� ��65   �� *TypeA+ � 	Resources 5*   *    �� 
TargetType �� Property �� Value��%V   '��       �� 5+   .   " ��64   .�5,   2   ��  � ��$ Center=�6X   5-   /   5.   +   5/   '   50   #   52        53      $
 Тип B��61   $ * 6@    54      �� 55   "   .R�56   &   �� ��65   �� *TypeB+ 57   *   ��%V   '��       �� 58   .   " ��64   .�59   2   ��  � ��$ Center=�6X   5:   /   5;   +   5<   '   5=   #   5?        5@      $
 Тип C��61   $ * 6@    5A      �� 5B   "   .R�5C   &   �� ��65   �� *TypeC+ 5D   *   ��%V   '��       �� 5E   .   " ��64   .�5F   2   ��  � ��$ Center=�6X   5G   /   5H   +   5I   '   5J   #   5L        5M      $
 
Сумма��6-   $ * 6V    6<   �� *Total+6V     5N      $
 Остаток��6-   $ * 6\    6>   �� *	Remaining+6\   5P      �� 5R       �� Orientation$ 
Horizontal=�6   $��Center=�66   $��0,20,0,0q�6S   .+�5S      �� -   5        $��
Отчет��5S      $	��120��5T      $��35��6!   $��0,0,20,0q�5U   0   �� 5V      -   5        $��Приход��5V      $	��120��5W      $��35��6!   5X      5Y      5Z      5[      5\      5]      