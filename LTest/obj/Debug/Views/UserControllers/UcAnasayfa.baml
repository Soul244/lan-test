   M S B A M L   `   `   `  ����	  LTest,    &LTest.Views.UserControllers.UcAnasayfa   5      U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      J FWpfAnimatedGif, Version=1.4.15.0, Culture=neutral, PublicKeyToken=null,gif"http://wpfanimatedgif.codeplex.com  5      .��5	      � .+�5
   
   � -   5          �Name$  AnaGrid��5
         SoftGrey#��[  68    � ColumnDefinitions 5      �� 5      -   5         ��Name$ sideBar��5   2   $	��320��6#   62   �� 5      .+�5      � $��0��6   $��0,0,5,0q�6#   
�������$��2��6G    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Opacity$
 0.20Q�5         	Direction$ 0Q�5         Color$ 	#FFF7F7F7��5         
BlurRadius$ 15Q�5         ShadowDepth$ 15Q�5      	   RenderingBias$	 Quality=�5      .+�5      �� -   5        
 ��Name$
 	menuStack��5      .+�5      �� -   5         
 Toggle#���  5      .��5      ��   IconBox#���  6$   $��16,0,0,0q�6F    J� OpacityMask 5      !� 5   "    j� Stretch$
 Fill=�6.     Menu !� Visual# [ 6=   5      5       ]� 5!        UstMenuDock#��[ 6    .+�5"      A� $��32��6"   $��32��6.   
���   �$
��Left=�6H   $
��-8,0q�6c   ��5#      �� 5$   "    �� ImageSource$? 9/LTest;component/Views/Images/default-kullanici-resmi.png��6-   $ UniformToFill=�6u   5&      �� 5'        
MenuButton#��[ 6!   $	��8,0q�6E   .��5(      ��   UstMenuLabel#���  6$   $��16(�6K   .��5)   "   �� -   5        $��KullaniciAdi��5)   ?   $��Giriş Yapın��6,   $��Center=�6S   5*      5+      5,      ]� 5-      $��48��6    5/      �� 50      -   5        #���  50      .��51      ]� #���  6$   .+�52      ��   IconBoxWithTrigger#���  6(    53   "   !� 54   &   $
 Fill=�62     Home# [ 6A   56      �� 57      #���  6$   .��58   "   �� $��	Ana Menü��6,   $��Center=�6<   59      5:      5;      �� 5<      -   5        #���  5<      .��5=      ]� #���  6$   .+�5>      �� #���  6(    5?   "   !� 5@   &   $
 Fill=�62    	 Test# [	 6A   5B      �� 5C      #���  6$   .��5D   "   �� $��Testler��6,   $��Center=�6;   5E      5F      5G      �� 5H      -   5        #���  5H      .��5I      ]� #���  6$   .+�5J      �� #���  6(    5K   "   !� 5L   &   $
 Fill=�62    

 Result# [
 6A   5N      �� 5O      #���  6$   .��5P   "   �� $��	Sonuçlar��6,   $��Center=�6<   5Q      5R      5S      5^      �� 5_      $��Bottom=�6   .+�5`      �� #���  6   .��5a      �� #���  6$   $��16,0,0,0q�6Q    5b      !� 5c   "   $
 Fill=�6.    	 About# [ 6=   5e      5f      �� 5g      -	   5        #���  5g      .��5h      �� #���  6$   $��16,0,0,0q�6Q    5i      !� 5j   "   $
 Fill=�6.     Setting# [ 6=   5l      5m      5n      5o      � 5q      -
   5        $
  grid��5q   #   $��1��6   $��2��61    � RowDefinitions 5r      �� 5s      �� 5t      $��48 �6$   .+�5w      �� $��0��6   .��5x      �� -   5         ��Name$ UcAnasayfaIcerik��5x   %   $��0,0,5,0q�6?   5y      �� 5|      $��1��6     	MainColor#���  6&     BorderStyle#��[ 6O   $��-5,0,0,0q�6t   ._�5}      � .+�5~      ]� $
��Left=�6$   .+�5      �� -   5         ��Name$ NotificationIcon��5   J   #���  6(   
�������$��24��6q   $��24��6|    5�   "   !� 5�   &   $
 Fill=�62   5�      �� 5�      -   5         ��Name$ NotificationLabel��5�   $    	 Label#���  6X   
�������$��5,0,0,0q�6�   5�      �� 5�      #��[  6!    �� CornerRadius$ 5��6H   $��5q�6Y   $	��110��6d   $��Right=�6p   ._�5�      ]� $��2q�6(   .+�5�   "   �� -   5         ��Name$ ActiveImage��5�   (   $��3,0,0,0q�6V   $��24��6g   $��24��6r   $
��Left=�6~   "  WpfAnimatedGif.ImageBehavior   RepeatBehavior$ 0x��6�   �� 5�   "   -   5        $ ActiveLabel��5�   (   $��Offline��6V   $��75��6h   
���   �$��24��6�   $��6,0,0,0q�6�   $��Titillium Web SemiBold)�6�   $��15(�6�   5�      5�      5�      5�      5�      5�      5�      5�      