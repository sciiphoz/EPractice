   M S B A M L   `   `   `  ����D  @EPractice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:EPractice.WindowsEPractice.Windows  5   	   #    EPractice.Windows.AdminWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:EPractice.Windows     5   	     � WindowState$  	Maximized=�5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � Title$ AdminWindow��5
   	   $	��550��6   $	��800��6*   .��5      �  � RowDefinitions 5   
   �� 5      $��1* �6   6*   �� 5      $��6* �6   6*   �� 5      $��1* �6   6*   .+�5   
   �� $��0��6   ��5      �� 5       �� Color 5      �� 5       �� R$ 36��6     �� G$ 29��6'    �� B$	 112��6.    �� A$	 255��66   6@   5      5      �� 5   
   $��2��6   ��5      �� 5       5      �� 5      $ 36��6    $ 29��6'   $	 112��6.   $	 255��66   6@   5!      5#      �� 5%   
   $��0��6   $
��Left=�6"   $��Center=�6=   .+�5&      �� -   5        	 ��Name$	 
ExitButton��5&   J   $��20 0 0 0q�6   $��5q�6'   $��75��63   $��30��6>   $��Arial)�6w   $
��Bold"�6�   
����������#3A32C7���#3A32C7.��6�   
Назад6�   5'      �� 5(   
   $��0��6   $��Right=�6"   $��Center=�6>   .+�5)      �� -   5        $	 LogOutButton��5)   J   $��0 0 20 0q�6   $��5q�6'   $��75��63   $��30��6>   $��Arial)�6{   $
��Bold"�6�   
����������#3A32C7���#3A32C7.��6�   LogOut6�   5*      �� 5,   
   $��0��6   $��Center=�6"   $��Center=�6?   .+�5-      �� $��Center=�6   
�������$
��Sans)�5.      $��SemiBold"�6   $��28(�60   .��6>   MARATHON SKILLS 20166T   5/       � 51   
   -   5        
  �Name$
 	MainFrame��51      $��1��6#     � NavigationUIVisibility$ Hidden=�60   6R   �� 53   
   $��2��6   $��Center=�6"   $��Center=�6?   .+�54      �� -   5         ��Name$ 
TimeLeftTB��54      $��Center=�6,   
�������$
��Sans)�6\   $��16(�6n   $��SemiBold"�6|   OM18 дней 8 часов и 17 минут до старта марафона!6�   55      56      57      