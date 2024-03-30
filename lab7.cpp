#include <iostream>
#include <string>

using namespace std;

// Function prototypes
bool isTerminal(char c);
bool parseS(string str, int& index);
bool parseA(string str, int& index);
bool parseB(string str, int& index);
bool parseC(string str, int& index);

bool isTerminal(char c) {
    return (c == 'a' || c == 'b' || c == 'c');
}

bool parseS(string str, int& index) {
    if (parseA(str, index) && parseB(str, index))
        return true;
    return false;
}

bool parseA(string str, int& index) {
    if (str[index] == 'a') {
        index++;
        return true;
    }
    return false;
}

bool parseB(string str, int& index) {
    if (str[index] == 'b') {
        index++;
        if (parseC(str, index))
            return true;
        return false;
    }
    return false;
}

bool parseC(string str, int& index) {
    if (str[index] == 'c') {
        index++;
        return true;
    }
    return true; // e transition
}

int main() {
    string input;
    cout << "Enter a string: ";
    cin >> input;

    int index = 0;
    if (parseS(input, index) && index == input.length())
        cout << "String is in the language." << endl;
    else
        cout << "String is not in the language." << endl;

    return 0;
}

