#include <string.h>
#include <ctype.h>
#include <stdio.h>

void identifyToken(char str[20], char type[20]) {
    printf("\n| %-15s | %-12s |", str, type);
}

void identifyOperator(char op) {
    printf("\n| %-15c | Operator     |", op);
}

int main() {
    char c, str[20];
    int num[100], lineno = 1, tokenvalue = 0, i = 0, j = 0, k = 0;

    printf("\nEnter the C program (Press Ctrl+D to end input):\n");

    while (1) {
        c = getchar();

        if (c == EOF || c == 4) {  // 4 is Ctrl+D
            break;
        }

        if (isdigit(c)) {
            tokenvalue = c - '0';
            c = getchar();
            while (isdigit(c)) {
                tokenvalue = tokenvalue * 10 + c - '0';
                c = getchar();
            }
            num[i++] = tokenvalue;
            ungetc(c, stdin);
        } else if (isalpha(c)) {
            putchar(c);
            c = getchar();
            while (isdigit(c) || isalpha(c) || c == '_' || c == '$') {
                putchar(c);
                c = getchar();
            }
            strcpy(str, "Identifier");
            ungetc(c, stdin);
            identifyToken(str, "Identifier");
        } else if (c == ' ' || c == '\t') {
            // Ignore whitespace
        } else if (c == '\n') {
            lineno++;
        } else {
            putchar(c);
            identifyOperator(c);
        }
    }

    printf("\n\nCategorized Elements:\n");

    printf("\n| Token         | Type          |");
    printf("\n| ------------- | ------------- |");

    printf("\n| Terminals     | Keywords      |");
    fseek(stdin, 0, SEEK_SET); // Reset input stream to read identifiers again

    k = 0;
    while (1) {
        c = getchar();

        if (c == EOF || c == 4) {  // 4 is Ctrl+D
            break;
        }

        if (isalpha(c)) {
            str[k++] = c;
            c = getchar();
            while (isdigit(c) || isalpha(c) || c == '_' || c == '$') {
                str[k++] = c;
                c = getchar();
            }
            str[k] = '\0';
            strcpy(str, "Terminal/Keyword");
            identifyToken(str, "Terminal/Keyword");
            k = 0;
        }
    }

    printf("\n| Identifiers   | Numbers       |");
    fseek(stdin, 0, SEEK_SET); // Reset input stream to read special characters again

    while (1) {
        c = getchar();

        if (c == EOF || c == 4) {  // 4 is Ctrl+D
            break;
        }

        if (isalpha(c)) {
            str[k++] = c;
            c = getchar();
            while (isdigit(c) || isalpha(c) || c == '_' || c == '$') {
                str[k++] = c;
                c = getchar();
            }
            str[k] = '\0';
            identifyToken(str, "Identifier");
            k = 0;
        } else if (isdigit(c)) {
            tokenvalue = c - '0';
            c = getchar();
            while (isdigit(c)) {
                tokenvalue = tokenvalue * 10 + c - '0';
                c = getchar();
            }
            num[i++] = tokenvalue;
            ungetc(c, stdin);
            identifyToken(str, "Number");
        }
    }

    printf("\n\nTotal number of lines: %d\n", lineno);

    return 0;
}

