//Fill out your copyright notice in the Description page of the Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Console/Cartridge.h"
#include "BullCowCartridge.generated.h"

UCLASS(ClassGroup=(Custom), meta=(BlueprintSpawnableComponent))
class BULLCOWGAME_API UBullCowCartridge : public UCartridge
{
	GENERATED_BODY()

	public;
	virtual void BeginPlay() override;
	TArray<FString> GetValidWords(const TArray<FString>& WordList) const;
	virtual void OnInput(const FString& PlayerInput) override;
	void ProcessGuess(const FString& Guess);
	bool IsIsogram(const FString& Word) const;
	void SetupGame();
	void EndGame();
	
	//Your declarations go below!
	private:
		bool bGameOver;
		TArray<FString> Words
		FString HiddenWord;
		int32 Lives;
};