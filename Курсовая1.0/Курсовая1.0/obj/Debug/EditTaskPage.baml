   M S B A M L   `   `   `  ����N  JКурсовая1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Курсовая1._0Курсовая1._0  5   	   '    !Курсовая1._0.EditTaskPage   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Курсовая1._0     5   	     � WindowState$  	Maximized=�5   	    � WindowStartupLocation$ CenterScreen=�6!    � 
ResizeMode$ 	CanResize=�6F    � Title$ EditTaskPage��5	   	   $	��450��6   $	��800��6+   .��5
      � ���	#FFE5E5E5 � ColumnDefinitions 5   
   �� 5      $	��400 �6   6-   �� 5      6!    � RowDefinitions 5   
   �� 5      $��50 �6   6*   �� 5      6   .+�5   
   �� -   5        $��TeacherComboBox��5      $
��Left=�6+   $��	10,10,0,0q�6F   $	��Top=�6Y   $	��173��6q   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��IssuanceDate��5      $
��Left=�6*   $��	222,8,0,0q�6E   $	��Top=�6X   $	��133��6p     5   
   -   5        $��DueDate��5   %   $��1��6   $
��Left=�66   $��0,9,0,0q�6Q   $	��Top=�6b   $	��132��6z   �� 5   
   -   5        $��
AddTaskBtn��5      $��Сохранить��6#   $��169,8,103,0q�67   $��1��6L   $	��Top=�6\   $��Arial)�6t   $��18(�6�   $	��128��6�   6�   �� 5   
   -   5        $��DescriptionTextBox��5   X   $��0��6   $��1��6"    �� AcceptsReturn .  �� TextWrapping$
 Wrap=�6D   $��80,40,80,180q�6r   $��2��6�   5      5      