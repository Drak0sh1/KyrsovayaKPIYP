   M S B A M L   `   `   `  ����N  JКурсовая1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Курсовая1._0Курсовая1._0  5   	   &     Курсовая1._0.TeacherPage   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Курсовая1._0     5   	     � Title$  TeacherPage��5   	   $	��406��6   $	��637��6*   .��5	      � $��0,0,0,24q�6   .+�5
   
   �� -   5        $��ExitBtn��5
      $=��7Вернуться на главную страницу��6    $
��Left=�6H   $��
380,10,0,0q�6c   $	��Top=�6w   $	��246��6�   $��32��6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��TeacherGrid��5         AutoGenerateColumns .  $��	0,0,273,0q�6B   
���   �   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$  Преподаватель��6%      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6N   �� *Name+6i   4  .System.Windows.Controls.DataGridTemplateColumn  5         CellTemplate 5      �� 5      .R�5      �� -   5        $ ��Редактировать��5   %   6W   5      5        5       5      �� 5      .R�5      �� -   5        $��Удалить��5   %   6Q   5      5      5      �� 5   
   -   5        $��AddBtn��5      $��Добавить��6   $
��Left=�62   $��
380,65,0,0q�6M   $	��Top=�6a   $	��137��6y   $��32��6�   6�   5       5!      