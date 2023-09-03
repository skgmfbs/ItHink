import 'package:english_words/english_words.dart';
import 'package:flutter/material.dart';

class BigCard extends StatelessWidget {

  final WordPair word;

  const BigCard({super.key,
    required this.word
  });

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    final style = theme.textTheme.displayMedium!.copyWith(
      color: theme.colorScheme.onPrimary
    );

    return Card(
      color: theme.colorScheme.primary,
        child: Padding(
            padding: const EdgeInsets.all(20.0),
            child: Text(
              word.asPascalCase,
              style: style
            )));
  }
}