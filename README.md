GenderCoder
===========

Utilities for determining gender based on first name (U.S. bias)

(Updated: 2014-08-18)

Gender Coder is a bit of a hard-and-fast gender identification solution optimized for handling large sets of input data.

Gender Coder's identification library is based on nam_dict.txt (referenced as GenderCodingNames.txt) from the open-source gender.c (http://www.heise.de/ct/ftp/07/17/182/), and is supplemented with a name list gleaned from 2010 US Census data. The supplemental list was constructed by combining lists of top female and male first names, removing any shared entries between the two lists, and removing any names found within nam_dict.txt . Names in the supplemental library default to strongly-typed male or female, depending on their dataset of origin (probably not ideal, but we're playing hard-and-fast, right?).

While GenderCoder.Processor contains a method to return a single GenderCodingResult, the solution is optimized to return results for large lists of names quickly through multi-threading the lookup process.

