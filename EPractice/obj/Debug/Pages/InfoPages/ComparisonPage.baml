   M S B A M L   `   `   `  ����D  @EPractice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:EPractice.Pages.InfoPagesEPractice.Pages.InfoPages  5      E'clr-namespace:EPractice.Pages.InfoPagesEPractice.Pages.InfoPages  5      .    (EPractice.Pages.InfoPages.ComparisonPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:EPractice.Pages.InfoPages     5      >
converters'clr-namespace:EPractice.Pages.InfoPages     5        N� Title$  ComparisonPage��5
       � 	Resources 5      4   .EPractice.Pages.InfoPages.NameToImageConverter   NameToImageConverter��%   &	          5   
   6%   5      .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��66 �6   �� 5      $��50 �6   �� 5      $��40 �6   �� 5      $��90 �6   �� 5      $��50 �6   �� 5      �� 5      $
��Auto �6   �� 5      $��25 �6    � ColumnDefinitions 5   
   �� 5      �� 5      $��162.034 �6   �� 5       $��157.966 �6   �� 5!      $��6 �6   �� 5"      $��178.288 �6   �� 5#      $��141.712 �6   �� 5$      .+�5'   
   �� $��124,10q�6   $
��Wrap=�6$   $��22(�68   
���   � �� TextAlignment$ Center=�6Y   $��5��6p   $��1��6�   $��1��6�   $5��/Насколько долгий марафон?��6�   �� 5(   
   -   5        $��txtName��5(      $��
14,10,10,3q�6#   $
��Wrap=�67   $��22(�6K   ���	#FF6C6C6C$ Center=�6p   $��2��6�   $��1��6�   $��2��6�   $.��(Имя выбранного пункта��6�   �� 5*   
   $��2��6   $��4��6)   $
��Left=�69   $��1��6T   $��6��6a   $	��263��6r   $��
57,61,0,10q�6~   .@�5+      �� $��Скорость��6   .��5,      � ���	#FFE5E5E5.+�5-      &   System.Windows.Controls.DataGrid  -   5          Name$ 	gridSpeed��5-      $
��Left=�62   $	��257��6M      HeadersVisibility$
 None=�6Y      AutoGenerateColumns .     SelectionMode$ Single=�6�   $��0,0,0,-2q�6�   	   Columns	 5.      4  .System.Windows.Controls.DataGridTemplateColumn  5/      ,  &System.Windows.Controls.DataGridColumn
   Header$

 Logo��65      Width5  /System.Windows.Controls.DataGridLengthConverter$ 1* 6C      CellTemplate 50   "   �� 51   &   .R�52   *   �� $	��100��60   ��6=   �� *Name+ �� 	Converter# [  53   '   55      0  *System.Windows.Controls.DataGridTextColumn  56      1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Name+58      59      5:      �� 5;      $��
Длина��6   .��5<      � ���	#FFE5E5E5.+�5=        -   5        $��gridDistance��5=      $
��Left=�63   $	��276��6N   $	��Top=�6[   $	��257��6s   $
 None=�6    .  $ Single=�6�   	 5>        5?      $

 Logo��65   $ 1* 6C    5@   "   �� 5A   &   .R�5B   *   �� $	��100��60   ��6=   �� *Name+# [  5C   '   5E        5F       61   �� *Name+5H      5I      5J      5K      �� 5M   
   -   5        $��txtInfo��5M      $��1��6#   $��65,39,65,10q�63   $��6��6H   $
��Wrap=�6U   $�� ��6i   $��2��6q   $��16(�6�   $ Center=�6�   �� 5N   
   -   5        $��imgInfo��5N      $��1��6   $	��169��6/   $��
10,10,10,0q�6<   $��3��6P   $��3��6]   $	��Top=�6n   $��2��6�   5O      5P      