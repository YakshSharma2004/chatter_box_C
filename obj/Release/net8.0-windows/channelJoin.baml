   M S B A M L   `   `   `  ����   sql_practice    sql_practice.channelJoin   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     N� Title$  Join Channels��.��� ����  �� 
StartPoint$	 0,0&� �� EndPoint$	 1,1&�.���  � Color$ 	#FF001F3F�� � Offset$ 0Q�� $ 	#FF00CFFF��$ 1Q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��📡 Join a Channel��$��0��$��26(�$
��Bold"�$��Segoe UI)�
�������$��Center=�$��	0,20,0,10q�� $��1��$��
20,0,20,10q� � ColumnDefinitions �� $��6* ��� $��2* ��� $
��1.5* �.+��� -   $��
search_txt��$��0��$��40��$��14(�$��8q�$��5q����	#FFF9F9F9���	#FF00BFFF$��1q��� -   $��
search_btn��$��Search��$��1��$��40��$��5q����	#FF00BFFF
�������$��SemiBold"�$��0q��� -   $��go_back��$
��Back��$��2��$��40��$��5q�
����� �
�������
����� ��� $��2��$��20q� �� CornerRadius$ 10�����	#33000000 �� Padding$ 10q�._��� $
��Auto=�.���� -   $��channel_list��
������ $��0q�
�������$��16(����� .R��� ���	#3300FFFF$ 8��$ 10q�$��6q�._��� 	 �� Orientation$	 
Horizontal=�$��Center=�.+��� $	 Vertical=�$	��400��.+��� $
��name��
�������$
��Bold"�$��16(����� *name+�� $��description��$��13(�
�������$
��Wrap=����� *description+�� $��join_btn��-   $
��Join��$��70��$��30��$��15,0,0,0q�
����� �
�������$
��Bold"�$��0q�