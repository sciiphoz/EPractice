   M S B A M L   `   `   `  ����D  @EPractice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:EPractice.Pages.ControllerPagesEPractice.Pages.ControllerPages  5      5    /EPractice.Pages.ControllerPages.SponsorInfoPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?local-clr-namespace:EPractice.Pages.ControllerPages     5        N� Title$  SponsorInfoPage��5	      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��10q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��0,0,0,10q�62   $��Center=�6D   .+�5      �� $!��Отсортировать:��6   $
��Bold"�62   $��Center=�6D   $��16(�6_   $��0,0,10,0q�6m   �� 5      -   5         ��Name$ SortComboBox��5      $	��200��61   $��25��6=   $��Center=�6I   .@�5      �� $!��Итоговая сумма��6#   
 � Tag$ TotalAmount��6<    �� 
IsSelected . 6N   �� 5      $-��'Название организации��6#   $ CharityName��6B   �� 5      $-��'Количество спонсоров��6#   $ SponsorsCount��6B   5      5      �� 5      $ 
Horizontal=�6   $��Center=�62   $
��0,10q�6O   .+�5      �� $@��:Благотворительные организации:��6   $
��Bold"�6B   $��0,0,5,0q�6T   �� 5      -   5         ��Name$ CharitiesCountText��5      $��0��68   �� 5      $7��1Всего спонсорских взносов:��6   $
��Bold"�6>   $��20,0,5,0q�6P   �� 5       -   5        $ TotalAmountText��5       $��$0��65   5!      5"      &   System.Windows.Controls.DataGrid  5$   
   -   5          Name$ SponsorsGrid��5$      $��1��6)   $��10q�66      AutoGenerateColumns .  	   CanUserAddRows	 .  
   CanUserDeleteRows
 .     
IsReadOnly .    HeadersVisibility$ Column=�5&         GridLinesVisibility$	 All=�6&   $
��Auto=�5'         Columns 5(      4  .System.Windows.Controls.DataGridTemplateColumn  5)      ,  &System.Windows.Controls.DataGridColumn   Header$ Логотип��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6:      CellTemplate 5*      �� 5+      .R�5,      �� $��75��6@   $��75��6K   $��Uniform=�6W   ��6$   �� *
LogoPath+6W   5-      5/      0  *System.Windows.Controls.DataGridTextColumn  50      $ Наименование��6%   $ 2* 6[   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *CharityName+6[     51      $ 
Сумма��6%   $ * 6d    64   �� *TotalAmount+ �� StringFormat$ C��6d     52      $- 'Количество спонсоров��6%   $ * 6e    6C   �� *SponsorsCount+6e   54      55      56      