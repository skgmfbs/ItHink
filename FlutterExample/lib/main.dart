import 'package:flutter/material.dart';
import 'package:flutter_example/models/app_state.dart';
import 'package:flutter_example/screens/favorite.dart';
import 'package:flutter_example/screens/login.dart';
import 'package:flutter_example/screens/word_random.dart';
import 'package:provider/provider.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return ChangeNotifierProvider(
      create: (_) => AppState(),
      child: MaterialApp(
        title: 'Flutter Demo',
        theme: ThemeData(
          colorScheme: ColorScheme.fromSeed(seedColor: Colors.yellow),
          useMaterial3: true,
        ),
        home: const MyHomePage(title: 'Flutter Demo Home Page'),
        // routes: ,
      ),
    );
  }
}

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
              WordRandomWidget(
                  word: appState.word,
                  onFavoriteButtonClicked: () {
                    appState.toggleFavorites();
                  },
                  onNextButtonClicked: () {
                    appState.randomWord();
                  },
                  isAdded: appState.isFavoriteAdded),
              const FavoriteWidget(),
              LoginWidget()
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
