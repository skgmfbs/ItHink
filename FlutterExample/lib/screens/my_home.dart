import 'package:flutter/material.dart';
import 'package:flutter_example/screens/favorite.dart';
import 'package:flutter_example/screens/login.dart';
import 'package:flutter_example/screens/word_random.dart';
import 'package:provider/provider.dart';

import '../models/app_state.dart';

class MyHomePage extends StatelessWidget {
  const MyHomePage({super.key, required this.title});

  final String title;

  @override
  Widget build(BuildContext context) {
    var appState = context.watch<AppState>();

    return Scaffold(
      body: Row(
        children: [
          Visibility(
            visible: MediaQuery.of(context).size.width >= 400,
            child: NavigationRail(
                extended: MediaQuery.of(context).size.width >= 400,
                destinations: const [
                  NavigationRailDestination(
                      icon: Icon(Icons.home), label: Text("Home")),
                  NavigationRailDestination(
                      icon: Icon(Icons.favorite), label: Text("Favorite")),
                  NavigationRailDestination(
                      icon: Icon(Icons.login), label: Text("Login"))
                ],
                selectedIndex: appState.selectedIndex,
                onDestinationSelected: (var index) {
                  appState.setSelectedIndex(index);
                }),
          ),
          Expanded(
              child: IndexedStack(
                index: appState.selectedIndex,
                children: [
                  WordRandomPage(
                      word: appState.word,
                      onFavoriteButtonClicked: () {
                        appState.toggleFavorites();
                      },
                      onNextButtonClicked: () {
                        appState.randomWord();
                      },
                      isAdded: appState.isFavoriteAdded),
                  const FavoritePage(),
                  LoginPage()
                ],
              ))
        ],
      ),
      bottomNavigationBar: Visibility(
        visible: MediaQuery.of(context).size.width <= 400,
        child: BottomNavigationBar(
          items: const [
            BottomNavigationBarItem(icon: Icon(Icons.home), label: "Home"),
            BottomNavigationBarItem(
                icon: Icon(Icons.favorite), label: "Favorite"),
            BottomNavigationBarItem(icon: Icon(Icons.login), label: "Login")
          ],
          currentIndex: appState.selectedIndex,
          selectedItemColor: Colors.amber[800],
          onTap: (index) {
            appState.setSelectedIndex(index);
          },
        ),
      ),
    );
  }
}