import 'package:flutter/material.dart';

import '../screens/e-commerce/home.dart';

class ECommerceApp extends StatelessWidget {
  const ECommerceApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: "E-Commerce example",
      theme: ThemeData(
        scaffoldBackgroundColor: Colors.white,
        colorScheme: ColorScheme.fromSeed(
          seedColor: Colors.white,
          primary: const Color(0xFF4C53A5),
          outline: const Color(0xFFEDECF2)

        ),
        useMaterial3: true,
      ),
      debugShowCheckedModeBanner: false,
      routes: {"/": (context) => const HomePage()},
    );
  }
}
