# GGXrdRevelator
Guilty Gear Xrd -REVELATOR- and -SIGN- UPK decrypter.

Also supports Arcana Heart 3: Love Max, LMSS, and LMSS Extend in SIGN mode.

Usage:

``GGXrdRevelatorDec [-e] (-sign|-revel) infile``

-e, when specified, encerypts the input instead of decrypting it (useful for modding, optional)

-sign works for Xrd -SIGN-, and any AH3 version including and after Love Max.  Specify -revel if you want to decrypt something from Revelator 1 or 2.

You need either -sign or -revel to exist or else the program will fail.

The output will be the input filename plus a ".dec" or ".enc" extension, depending on if you encrypted or decrypted.

Special thanks to Altimor (which made the GG Xrd SIGN decryption tool) and gdkchan (who released this source + inadvertently solved AH3 encryption).