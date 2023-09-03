import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models/app_state.dart';

class FavoriteWidget extends StatelessWidget {
  const FavoriteWidget({super.key});

  @override
  Widget build(BuildContext context) {
    var appState = context.watch<AppState>();
    return Center(
      child: ListView(
          children: appState.favorites
              .map((e) => Center(
                      child: ListTile(
                          title: Text(
                    e.asString,
                    textAlign: TextAlign.center,
                  ))))
              .toList()),
    );
  }
}
