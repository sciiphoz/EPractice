   M S B A M L   `   `   `  ����D  @EPractice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:EPractice.Pages.AdminPagesEPractice.Pages.AdminPages  5      ,    &EPractice.Pages.AdminPages.OrgEditPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:EPractice.Pages.AdminPages     5        N� Title$  OrgEditPage��5	      .��5      � .+�5   
   �� $��20q�6   .+�5      �� $��0,0,0,20q�5      $��Center=�5      $��20(�5      $
��Bold"�5      ���#241D70$q��kДобавление/Редактирование благотворительных организаций��5      �� 5      $��0,10,0,5q�5      $��14(�5      $
��Bold"�5      ���#241D70$��Наименование:��5      �� 5      -   5         ��Name$ NameTextBox��5      $��40��5      $��10q�5      $��14(�5       ��5!      �� *CharityName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5#      $��0,20,0,5q�5$      $��14(�5%      $
��Bold"�5&      ���#241D70$��Описание:��5(      �� 5*      -   5        $ DescriptionTextBox��5+      $	��120��5,      $��10q�5-      $��14(�5.       �� TextWrapping$
 Wrap=�5/       �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�51      ��52      �� *CharityDescription+$ TwoWay=��� 54      $��0,20,0,5q�55      $��14(�56      $
��Bold"�57      ���#241D70$��Логотип файл:��59      �� 5;      $��0,0,0,10q�5<      $��12(�5=      $��Italic$�5>      ���#241D70$����Оставьте поле 'Логотип файл' пустым, если вы не хотите загружать логотип��5@      � 5B       � ColumnDefinitions 5C      �� 5D      $��* �6'   �� 5E      $
��Auto �6'   .+�5H      �� -   5        $ LogoPathTextBox��5I      $��0��5J      $��40��5K      $��10q�5L      $��14(�5M       �� 
IsReadOnly . ��5O      �� *NewLogoPath+$ OneWay=��� 5Q      -   5        $��1��5R      $	��120��5S      $��40��5T      $��10,0,0,0q�5U      $��Просмотр...��5W      $
��Bold"�5X      
�������5Y      5Z      �� 5]      $��0,20,0,5q�5^   	   $��14(�5_   	   $
��Bold"�5`   	   ���#241D70$$��Текущий логотип:��5b   	   �� 5d      -   5        	 ��Name$	 CurrentLogoImage��5e   	   $	��120��5f   	   $	��120��5g   	   $��Uniform=�5h   	   ��5i   	   �� *CurrentLogoPath+�� 5l      $��0,30,0,0q�5m   	   $��Center=�5n   	   
 �� Orientation$
 
Horizontal=�5o   	   .+�5p      �� -   5        $	��120��5q      $��40��5r      $��Сохранить��5t      $
��Bold"�5u      
�������5v      �� 5x      -   5        $	��120��5y      $��40��5z      $��20,0,0,0q�5{      $��Отмена��5}      $
��Bold"�5~      5      5�      5�      5�      