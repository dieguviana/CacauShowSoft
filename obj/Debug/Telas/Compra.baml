   M S B A M L   `   `   `  ����  NewAppCacauShow     SistemaDeCompra.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Tela de Compra��5   	   $	��400��6    $	��300��6,   .��5      �  � ColumnDefinitions 5   
   �� 5      $��3* �6   �� 5      $	��37* �6   .+�5
   
   �� $��20,20,20,20q�6   $��2��6*   .+�5      �� $��Produto:��6   �� 5      -   5         ��Name$ 
txtProduto��5      $	��200��6*   $	��0,5q�66   �� 5      $��Quantidade:��6   �� 5      -   5        $ txtQuantidade��5      $	��100��6-   $	��0,5q�69   �� 5      $��Preço unitário:��6   �� 5      -   5        $ txtPrecoUnitario��5      $	��100��60   $	��0,5q�6<   �� 5      -   5        $��Adicionar ao Carrinho��5      $	��150��6W   $
��0,10q�6c   �� 5      -   5         ��Name$ 
lvCarrinho��5      $
��0,10q�6+    �� View 5      �� 5      .P�5      �� $��Produto��6)    �� Width$	 150��6:    �� DisplayMemberBinding 6F   �� *	Produto+�� 5      $��
Quantidade��6)   $	 100��6=    6I   �� *
Quantidade+�� 5      $��Preço Unitário��6)   $	 100��6A    6M   �� *PrecoUnitario+�� 5      $��Subtotal��6)   $	 100��6;    6G   �� *
Subtotal+5      5      �� 5!      -   5         ��Name$ lblTotal��5!      $��Total: R$ 0.00��6&   $
��0,10q�6?   5"      5#      5$      