   M S B A M L   `   `   `  ����D  @EPractice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:EPractice.Pages.RunnerPagesEPractice.Pages.RunnerPages  5      0    *EPractice.Pages.RunnerPages.MySponsorsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:EPractice.Pages.RunnerPages     5        N� Title$  MySponsorsPage��5	      .��5      � .+�5   
   �� $��1��6   $
��Auto=�6$   .��5      �� $��40,20,40,20q�6   .+�5      �� $��Мои спонсоры��6   $��Center=�6/   $��24(�6L   $
��Bold"�6Z   �� 5      $
��Wrap=�6   $��	0 10 0 20q�60    �� TextAlignment$ Center=�6C   $��20(�6Z   WUЗдесь показаны все ваши спонсоры в Marathon Skills 2016.5      �� 5      $��0,0,0,20q�6   .+�5      �� $Y��SНаименование благотворительной организации:��6   $��16(�6R   $
��Bold"�6`   �� 5      -   5         ��Name$ CharityNameText��5       $��20(�69   $��0,5,0,0q�6G   � 5      $��0,10,0,0q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6/   �� 5      $��* �6/   .+�5      �� $	��100��6!   $	��100��6-   
�������$��1q�6R    �� CornerRadius$ 5��5      
�������$��0��6=   ._�5      �� -   5         ��Name$ CharityLogoImage��5   $   $��Uniform=�6>   5       �� 5"      -   5        $ CharityDescriptionText��5"   $   $
��Wrap=�6D   $��10,0,0,0q�6X   $��1��5#      $ Justify=�6,   5$      5%      �� 5'      .+�5(      �� -   5        $ NoSponsorsText��5(       $3��-У вас пока нет спонсоров.��68   $y�	Collapsed=�5)      $ Center=�63   $��Italic$�6J   $��	0,20,0,20q�6]   &   System.Windows.Controls.DataGrid  5+      -   5          Name$ SponsorsGrid��5+         AutoGenerateColumns .     CanUserAddRows .  	   CanUserDeleteRows	 .  
   
IsReadOnly
 .    HeadersVisibility$ Column=�5,   G   $��0q�5-         GridLinesVisibility$ 
Horizontal=�6/   $��	0,10,0,20q�6P      Columns 5.      4  .System.Windows.Controls.DataGridTemplateColumn  5/      ,  &System.Windows.Controls.DataGridColumn   Header$
 Logo��65      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6C      CellTemplate 50   "   �� 51   &   .R�52   *   �� $��40��6M   $��40��6X   $��Uniform=�6d   ��60   �� *	LogoImage+6d   53   '   55      0  *System.Windows.Controls.DataGridTextColumn  56      $ Спонсор��61   $ * 6b   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6B   �� *SponsorName+6b     57      $ 
Взнос��61   $
 Auto 6p    6@   �� *Amount+ �� StringFormat$
 ${0}��6p   59      5:      �� 5<       �� Orientation$ 
Horizontal=�6   $��Right=�66   $��0,10,0,0q�6R   .+�5=      �� $��Всего ��6    $
��Bold"�6.   �� 5>      -   5        $ TotalAmountText��5>       $
��Bold"�69   $��$0��6K   5?      5@      5A      5B      5C      