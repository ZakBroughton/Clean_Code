   M S B A M L   `   `   `  ����K  GAssignment-2-GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Assignment_2_GUIAssignment_2_GUI  5   	   I)clr-namespace:Assignment_2_GUI.ViewModelsAssignment_2_GUI.ViewModels  66   "    Assignment_2_GUI.AddItemForm   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Assignment_2_GUI     5   	   @
viewmodels)clr-namespace:Assignment_2_GUI.ViewModels     66     � Title$  AddItemForm��5   	   $	��350��6   $	��500��6*   .��5	      �� $��10q�6   .+�5
   
   ��  �� Orientation$ 
Horizontal=�6   $��	0,10,0,10q�6.   $��Center=�6A   .+�5      �� $��Employee Name��6   $	��100��6,   $��Center=�68   �� 5      $	��200��6Z   ��6   �� *EmployeeName+ �� UpdateSourceTrigger$ PropertyChanged=�6Z   5      �� 5   
   $ 
Horizontal=�6   $��	0,10,0,10q�6.   $��Center=�6A   .+�5      �� $��	Item Name��6   $	��100��6(   $��Center=�64   �� 5      $	��200��6U   ��6   �� *
ItemName+$ PropertyChanged=�6U   5      �� 5   
   $ 
Horizontal=�6   $��	0,10,0,10q�6.   $��Center=�6A   .+�5      �� $��Quantity��6   $	��100��6'   $��Center=�63   �� 5      $	��200��6U   ��6   �� *
Quantity+$ PropertyChanged=�6U   5      �� 5   
   $ 
Horizontal=�6   $��	0,10,0,10q�6.   $��Center=�6A   .+�5      �� $��Price��6   $	��100��6$   $��Center=�60   �� 5      $	��200��6   ��6   �� *Price+$ PropertyChanged=� �� TargetNullValue$  �� �� StringFormat$ {0:F2}��6   5      �� 5   
   ��6   �� *	Message+�� 5   
   $��Add Item��64   $��0,0,0,0q�6H   $	��100��6Y   $��Center=�6e   ��6   �� *AddItemCommand+6e   5      5      