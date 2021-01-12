//Fill out your copyright notice in the Description page of the Project Settings.
#include "BullCowCartridge.h"

void UBullCowCartridge::BeginPlay() //Start
{
	Super::BeginPlay();
	PrintLine("Hi There! Welcome to the Bull Cow Game");
	PrintLine("Press Enter to continue. . .")
}

void UBullCowCartridge::OnInput(const FString& Input) //On Enter
{
	ClearScreen();
}