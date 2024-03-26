import 'package:flutter/material.dart';

class LoginPage extends StatelessWidget {
  LoginPage({super.key});

  // var usernameController = TextEditingController();
  // var passwordController = TextEditingController();

  final formKey = GlobalKey<FormState>();

  @override
  Widget build(BuildContext context) {
    // var appState = context.watch<AppState>();
    return Padding(
      padding: const EdgeInsets.all(40.0),
      child: Form(
        key: formKey,
        autovalidateMode: AutovalidateMode.disabled,
        child: Column(
          children: [
            const SizedBox(
              height: 100,
            ),
            // TextField(
            //   decoration: const InputDecoration(
            //       hintText: 'username', border: OutlineInputBorder()),
            //   controller: usernameController,
            // ),
            TextFormField(
                key: const ValueKey("username"),
                decoration: const InputDecoration(
                    hintText: 'username', border: OutlineInputBorder()),
                validator: (value) {
                  if (value == null || value.isEmpty) {
                    return 'Please enter some text';
                  }
                  return null;
                }),
            const SizedBox(
              height: 10,
            ),
            // TextField(
            //   obscureText: true,
            //   decoration: InputDecoration(
            //       hintText: 'password',
            //       border: const OutlineInputBorder(),
            //       // labelText: 'Enter the Value',
            //       errorText: !appState.loginFormValid
            //           ? 'Value Can\'t Be Empty'
            //           : null),
            //   controller: passwordController,
            // ),
            TextFormField(
                key: const ValueKey("password"),
                obscureText: true,
                decoration: const InputDecoration(
                    hintText: 'password', border: OutlineInputBorder()),
                validator: (value) {
                  if (value == null || value.isEmpty) {
                    return 'Please enter some text';
                  }
                  return null;
                }),
            const SizedBox(
              height: 10,
            ),
            ElevatedButton(
                onPressed: () {
                  if (formKey.currentState!.validate()) {
                    ScaffoldMessenger.of(context).showSnackBar(
                      const SnackBar(content: Text('Processing Data')),
                    );
                  }
                  // appState.loginFormValid = passwordController.text.isEmpty;
                },
                child: const Text("Login"))
          ],
        ),
      ),
    );
  }
}
