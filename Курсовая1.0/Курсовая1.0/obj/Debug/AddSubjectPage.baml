   M S B A M L   `   `   `  ����N  JКурсовая1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Курсовая1._0Курсовая1._0  5   	   )    #Курсовая1._0.AddSubjectPage   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Курсовая1._0     5   	     � Title$  AddSubjectPage��5   	   $	��450��6    $	��500��6-   .��5	      � ���	#FFF5F5F5.+�5   
   �� ���	#FF3F51B5$��60��6(   $	��Top=�64   ._�5      �� $F��@Добавление предмета преподавателю��6   
�������$��22(�5      $
��Bold"�5      $��Center=�5      $��Center=�5      5      � 5   
   $��20,80,20,20q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� $2��,Выберите преподавателя:��6   $��0��5       $
��Left=�5!      $��	20,0,0,10q�5"      $��14(�5#      �� 5%      -   5        $��Teacher��5%      $��1��5&      $��Stretch=�5'      $��
20,0,20,20q�5(      $��35��5)      $��14(�5*      $��5q�5+         ModernComboBox2#��[  5,      �� 5/      $(��"Название предмета:��6   $��2��50      $
��Left=�51      $��	20,0,0,10q�52      $��14(�53      �� 55      -   5        $��Subject��55      $��3��56      $��Stretch=�57      $��
20,0,20,20q�58      $��35��59      $��14(�5:      $��5q�5;        ModernTextBox2#��[ 5<      �� 5?      $&�� Количество часов:��6   $��3��5@      $
��Left=�5A      $��
20,45,0,25q�5B      $��14(�5C      $��2��6"   �� 5E      -   5        $
��Hour��5E      $��4��5F      $��20,28,20,-8q�5G      $��35��5H      $��14(�5I      $��5q�5J      #��[ 5K      �� 5N      -   5        $��Добавить��5N      $��4��5O      $��Center=�5P      $��
0,82,0,-62q�5Q      $	��200��5R      $��35��5S      $��16(�5T        ModernButton2#��[ 5U      5V      5W      5X      5Y      