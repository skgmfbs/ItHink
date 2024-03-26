import 'package:english_words/english_words.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_example/components/big_card.dart';

class NavigationApp extends StatelessWidget {
  const NavigationApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        // This is the theme of your application.
        //
        // TRY THIS: Try running your application with "flutter run". You'll see
        // the application has a blue toolbar. Then, without quitting the app,
        // try changing the seedColor in the colorScheme below to Colors.green
        // and then invoke "hot reload" (save your changes or press the "hot
        // reload" button in a Flutter-supported IDE, or press "r" if you used
        // the command line to start the app).
        //
        // Notice that the counter didn't reset back to zero; the application
        // state is not lost during the reload. To reset the state, use hot
        // restart instead.
        //
        // This works for code too, not just values: Most code changes can be
        // tested with just a hot reload.
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.yellow),
        useMaterial3: true,
      ),
      home: const MyHomePage(title: 'Flutter Demo Home Page'),
      // routes: ,
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key, required this.title});

  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.

  // This class is the configuration for the state. It holds the values (in this
  // case the title) provided by the parent (in this case the App widget) and
  // used by the build method of the State. Fields in a Widget subclass are
  // always marked "final".

  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
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
