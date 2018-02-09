#include <math.h>
#include <iostream>
using namespace std;

long double timeToScore(long double time) {
    return (.0000000001/time)*10000000000000;
}

int main(int argc, const char * argv[]) {
    if(argc < 2) {
        cout << "Not enough arguments!\n";
        return 0;
    }

    cout << timeToScore(atof(argv[1])) << "\n";
    return 0;
}
