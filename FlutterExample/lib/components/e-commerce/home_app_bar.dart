import 'package:flutter/material.dart';
import 'package:badges/badges.dart' as badges;

class HomeAppBar extends StatelessWidget {
  const HomeAppBar({super.key});

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    return Container(
      color: Colors.white,
      padding: const EdgeInsets.all(25),
      child: Row(
        children: [
          Icon(
            Icons.sort,
            size: 30,
            color: theme.colorScheme.primary,
          ),
          Padding(
            padding: const EdgeInsets.only(left: 20),
            child: Text(
              "Soccer shoes shop",
              style: TextStyle(
                fontSize: 23,
                fontWeight: FontWeight.bold,
                color: theme.colorScheme.primary,
              ),
            ),
          ),
          const Spacer(),
          badges.Badge(
            badgeStyle: const badges.BadgeStyle(
                badgeColor: Colors.red, padding: EdgeInsets.all(7)),
            badgeContent: const Text(
              "3",
              style: TextStyle(color: Colors.white),
            ),
            child: InkWell(
              // onTap: (){},
              child: Icon(
                Icons.shopping_bag_outlined,
                size: 30,
                color: theme.colorScheme.primary,
              ),
            ),
          )
        ],
      ),
    );
  }
}
