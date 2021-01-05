#include <iostream>

int main()
{
	std::cout << "You have a suitcase with a 3 digit combo lock" << std::endl;
	std::cout << "Read the clues and input the prospective code" << std::endl;

	const int a = 4; //Const prefix = unchanging var!
	const int b = 2;
	const int c = 7;

	int sum = a + b + c;
	int product = a * b * c;

	std::cout << sum << std::endl;
	stda::cout << product << std::endl;

	return 0;
}