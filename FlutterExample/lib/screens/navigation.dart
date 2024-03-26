import 'package:english_words/english_words.dart';
import 'package:flutter/material.dart';

import '../components/big_card.dart';

class NavigationPage extends StatefulWidget {
  const NavigationPage({super.key, required this.title});

  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.

  // This class is the configuration for the state. It holds the values (in this
  // case the title) provided by the parent (in this case the App widget) and
  // used by the build method of the State. Fields in a Widget subclass are
  // always marked "final".

  final String title;

  @override
  State<NavigationPage> createState() => _NavigationPageState();
}

class _NavigationPageState extends State<NavigationPage> {
  var word = WordPair.random();
  var favorites = <WordPair>[];
  var selectedIndex = 0;

  @override
  Widget build(BuildContext context) {
    Widget page;
    switch (selectedIndex) {
      case 0:
        page = buildRandomPage();
        break;
      case 1:
        page = buildFavoritePage();
        break;
      default:
        throw UnimplementedError("Error");
    }

    return Scaffold(
      body: Row(
        children: [
          NavigationRail(
              extended: MediaQuery.of(context).size.width >= 10000,
              destinations: const [
                NavigationRailDestination(
                    icon: Icon(Icons.home), label: Text("Home")),
                NavigationRailDestination(
                    icon: Icon(Icons.favorite), label: Text("Favorite"))
              ],
              selectedIndex: selectedIndex,
              onDestinationSelected: (var index) {
                setState(() {
                  selectedIndex = index;
                });
              }),
          Expanded(child: page)
        ],
      ),
    );
  }

  Widget buildRandomPage() {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          const Text("Random word program"),
          BigCard(word: word),
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              ElevatedButton(
                  onPressed: () {
                    setState(() {
                      if (favorites.contains(word)) {
                        favorites.remove(word);
                      } else {
                        favorites.add(word);
                      }
                    });
                  },
                  child: favorites.contains(word)
                      ? const Text("Remove")
                      : const Text("Add to favorites")),
              const SizedBox(
                width: 10,
              ),
              ElevatedButton(
                  onPressed: () {
                    setState(() {
                      word = WordPair.random();
                    });
                  },
                  child: const Text("Next"))
            ],
          ),
          ElevatedButton(
              onPressed: () {
                Navigator.push(context,
                    MaterialPageRoute(builder: (context) {
                      return Scaffold(
                          appBar: AppBar(title: const Text("Page")),
                          body: buildFavoritePage());
                    }));

                // Navigator.of(context).pop();
              },
              child: const Text("Favorite page"))
        ],
      ),
    );
  }

  Widget buildFavoritePage() {
    return Center(
      child: ListView(
        // children:
        // favorites.map((e) => ListTile(title: Text(e.asString))).toList()
          children: favorites
              .map((e) => Center(child: ListTile(title: Text(e.asString))))
              .toList()),
    );
  }
}