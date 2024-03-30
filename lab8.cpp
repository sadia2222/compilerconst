#include <iostream>
#include <string>
#include <cctype>

using namespace std;

enum State { q0, q1, q2, q3 };

bool isValidVariable(const string& variable) {
    State currentState = q0;
    for (size_t i = 0; i < variable.length(); ++i) {
        char c = variable[i];
        switch (currentState) {
            case q0:
                if (isalpha(c) || c == '_')
                    currentState = q1;
                else
                    return false;
                break;
            case q1:
            case q2:
                if (isalnum(c) || c == '_')
                    currentState = q2;
                else
                    return false;
                break;
            case q3:
                return false;
        }
    }
    return currentState == q2;
}

int main() {
    string variable;
    cout << "Enter a variable name: ";
    cin >> variable;

    if (isValidVariable(variable))
        cout << "Valid variable name." << endl;
    else
        cout << "Invalid variable name." << endl;

    return 0;
}

