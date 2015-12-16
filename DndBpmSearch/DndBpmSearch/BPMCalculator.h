#pragma once

#include "fmod.hpp"
#include "fmod.h"
#include "fmod_errors.h"
#include "fmod_common.h"
#include "fmod_codec.h"
#include "fmod_output.h"
#include <iostream>
#include <ctime>
#include <windows.h>
#include <vector>
#include <array>


class BPMCalculator {

public:
	FMOD::System		*system				= NULL;
	FMOD::Sound			*sound				= NULL;
	FMOD::Channel		*channel			= 0;
	FMOD::ChannelGroup	*channelGroup		= NULL;
	FMOD::ChannelGroup	*masterChannelGroup = NULL;
	FMOD::DSP			*fftdsp;

	BPMCalculator (  );

	void loadSound ( const char *path );
	void playSound ( );

	void calculateSpectrum ( );

	void FMODError ( FMOD_RESULT result ) {
		if ( result != FMOD_OK ) {
			printf ( "FMOD error! (%d) %s\n", result, FMOD_ErrorString ( result ) );
			std::getchar ( );
			//exit ( -1 );
		}
	}

	~BPMCalculator ( );
};
