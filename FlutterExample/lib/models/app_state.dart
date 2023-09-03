import 'package:english_words/english_words.dart';
import 'package:flutter/widgets.dart';

class AppState extends ChangeNotifier {
  var word = WordPair.random();
  var favorites = <WordPair>[];
  var selectedIndex = 0;
  // var loginFormValid = false;

  void randomWord() {
    word = WordPair.random();
    notifyListeners();
  }

  void toggleFavorites() {
    if (favorites.contains(word)) {
      favorites.remove(word);
    } else {
      favorites.add(word);
    }
    notifyListeners();
  }

  void setSelectedIndex(int index) {
    selectedIndex = index;
    notifyListeners();
  }

  get isFavoriteAdded => favorites.contains(word);
}
