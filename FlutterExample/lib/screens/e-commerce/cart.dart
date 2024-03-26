import 'package:flutter/material.dart';
import 'package:flutter_example/components/e-commerce/cart_app_bar.dart';

import '../../components/e-commerce/cart_item_samples.dart';

class CartPage extends StatelessWidget {
  const CartPage({super.key});

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    return Scaffold(
      body: ListView(
        children: [
          const CartAppBar(),
          Container(
              height: 700,
              padding: const EdgeInsets.only(top: 15),
              decoration: BoxDecoration(
                  color: theme.colorScheme.outline,
                  borderRadius: const BorderRadius.only(
                      topLeft: Radius.circular(35),
                      topRight: Radius.circular(35))),
              child: Column(
                children: [
                  const CartItemSamplesWidget(),
                  Container(
                    margin: const EdgeInsets.symmetric(
                        vertical: 20, horizontal: 15),
                    padding: const EdgeInsets.all(10),
                    child: Row(
                      children: [
                        Container(
                          decoration: BoxDecoration(
                              color: theme.colorScheme.primary,
                              borderRadius: BorderRadius.circular(20)),
                          child: Icon(
                            Icons.add,
                            color: theme.colorScheme.background,
                          ),
                        ),
                        Padding(
                          padding: const EdgeInsets.symmetric(
                              vertical: 10, horizontal: 10),
                          child: Text(
                            "Add Coupon Code",
                            style: TextStyle(
                                fontSize: 16,
                                color: theme.colorScheme.primary,
                                fontWeight: FontWeight.bold),
                          ),
                        )
                      ],
                    ),
                  )
                ],
              ))
        ],
      ),
    );
  }
}
