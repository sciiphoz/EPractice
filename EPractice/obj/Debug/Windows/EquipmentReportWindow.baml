   M S B A M L   `   `   `  ����D  @EPractice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:EPractice.WindowsEPractice.Windows  5   	   -    'EPractice.Windows.EquipmentReportWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:EPractice.Windows     5   	     � Title$  EquipmentReportWindow��5   	   $	��450��6'   $	��800��64   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $2��,Отчет прихода инвентаря��6!   $��20(�6@   $
��Bold"�6N   $��Center=�5      $
��0,10q�6)   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ ReportDataGrid��5      $��1��6+   $��10q�68      AutoGenerateColumns .     
IsReadOnly .    HeadersVisibility$ Column=�5         CanUserAddRows .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Наименование��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6X   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6;   �� *
ItemName+6X     5      $ Остаток��6%   $	 * 6W   
 66   �� *CurrentStock+6W     5      $ Необходимо��6%   $	 * 6V   
 69   �� *
Required+6V     5      $ Надо заказать��6%   $	 * 6X   
 6<   �� *	ToOrder+   ElementStyle 5      �� 5       �� 
TargetType" ��6    .�5      ��  �� Property � �� �� Value 6;   �� *ToOrderBackground+�� 5       � ��$
 Bold"�6;   5      5!      5#      �� 5%   
   -   5        $��2��5%      $��Печать��6   $	��100��6/   $��30��6;   $
��0,10q�6G   $��Center=�5&   	   6&   5'      5(      