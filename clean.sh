#!/usr/bin/env bash

source ./CONFIG.inc

clean() {
	local DLL=$1.dll

	find ./bin -name "$DLL" -delete
	find ./obj -name "$DLL" -delete
	rm -f "./GameData/$DLL"
	rm -f "./GameData/$TARGETBINDIR/$DLL"
	rm -f "$LIB/$DLL"
	rm -f "${KSP_DEV}/GameData/$DLL"
	rm -f "${KSP_DEV}/GameData/$TARGETBINDIR/$DLL"
}

VERSIONFILE=$PACKAGE.version

rm -f "./GameData/$TARGETDIR/$VERSIONFILE"
rm -f "./GameData/$TARGETDIR/*.md"
rm -f "./GameData/$TARGETDIR/NOTICE"
rm -f "./GameData/$TARGETDIR/"LICENSE*
rm -f "./GameData/$TARGETDIR/"*.pdf
for dll in $PACKAGE ; do
    clean $dll
done
