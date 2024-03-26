import 'package:english_words/english_words.dart';
import 'package:flutter/material.dart';
import '../components/big_card.dart';

class WordRandomPage extends StatelessWidget {
  final WordPair word;
  final VoidCallback? onFavoriteButtonClicked;
  final VoidCallback? onNextButtonClicked;
  final bool isAdded;

  const WordRandomPage(
      {super.key,
      required this.word,
      required this.onFavoriteButtonClicked,
      required this.onNextButtonClicked,
      required this.isAdded});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
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
                  onPressed: onFavoriteButtonClicked,
                  child: isAdded
                      ? const Text("Remove")
                      : const Text("Add to favorites")),
              const SizedBox(
                width: 10,
              ),
              ElevatedButton(
                  onPressed: onNextButtonClicked,
                  child: const Text("Next"))
            ],
          ),
          ElevatedButton(
              onPressed: () {
                // Navigator.push(context, MaterialPageRoute(builder: (context) {
                //   return Scaffold(
                //       appBar: AppBar(title: const Text("Page")),
                //       body: buildFavoritePage());
                // }));

                // Navigator.of(context).pop();
              },
              child: const Text("Favorite page"))
        ],
      ),
    );
  }
}
