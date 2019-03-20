#include <iostream>
#include <string>

using std::string;
using std::cout;

int main(int argc, const char * argv[]) {
  if(argc < 2 ) {
    cout << "USAGE: " << argv[0] << " <string>\n";
    exit(1);
  }

  string input(argv[1]);
  for(char & c: input) {
    cout << (char) ((c > 64 && c < 91) ? c^32 : c);
  }

  cout << '\n';
}
