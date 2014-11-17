import csv
import os

shorthandToPrefabNames = { "CTL": "cornerMinusXPlusZ"
                         , "CTR": "cornerPlusXPlusZ"
                         , "CBL": "cornerMinusXMinusZ"
                         , "CBR": "cornerPlusXMinusZ"
                         , "SL" : "sideMinusX"
                         , "ST" : "sidePlusZ"
                         , "SR" : "sidePlusX"
                         , "SB" : "sideMinusZ"
                         , "F"  : "flat2x2"
                         , "F2" : "flat4x4"
                         }
                         
class Placement:
	def __init__( self, shorthand ):
		tileType = shorthand.split( "-" )[0].upper()
		self.partName = shorthandToPrefabNames[tileType]
		self.x = 0
		self.z = 0
		self.y = 0
	
	def __str__(self):
		outStr = """<part type="%s" x="%i" y="%i" z="%i" />"""
		return outStr % ( self.partName, self.x, self.y, self.z )
	
	def __cmp__(self, other):
		if self.x < other.x:
			return -1
		if self.x > other.x:
			return 1
		if self.z < other.z:
			return 1
		if self.z > other.z:
			return -1
		if self.partName < other.partName:
			return -1
		if self.partName > other.partName:
			return 1
		return 0

def main():
	filesToConvert = [p for p in os.listdir( "." ) if p.endswith( ".csv" )]
	for fileName in filesToConvert:
		try:
			writeXMLForCsv( fileName )
		except Exception as e:
			print "Error processing file: %s\n%s" % ( fileName, e )
			raise e

def writeXMLForCsv( inFileName ):
	outFileName = "../Scratch/" + inFileName.replace( ".csv", ".xml" )
	with open(outFileName, "w") as outFile:
		rows = []			
		with open(inFileName, 'rU') as csvFile:
			reader = csv.reader(csvFile)
			for row in reader:
				rows.append( row )
		placements = computePlacementsFromRows( rows )
		placements.sort()
		outFile.write( """<?xml version="1.0" encoding="UTF-8" ?>""" );
		outFile.write( "\n" )
		outFile.write( """<hole name="%s">\n""" % inFileName )
		for placement in placements:
			outFile.write( "\t" )
			outFile.write( str( placement ) )
			outFile.write( "\n" )
		outFile.write( "</hole>\n" )
		print "Completed %s" % os.path.abspath( outFileName )

def computePlacementsFromRows( rows ):
	# first, find row,col for "START" tile. it will be
	# assigned to 0,0.
	result = []
	startRow, startCol = findStart( rows )
	for rowIter, row in enumerate( rows ):
		for colIter, value in enumerate( row ):
			if len( value.strip() ) > 0:
				p = Placement( value )
				# a little weird logic: we know all the prefab
				# tiles are based on 2x2 world units. so that's the 2x here.
				#
				# also I want the ball to progress downward in game;
				# that is, starts are generally at the top (in row 0, say)
				# but as the row number increases, we want the tile's Z
				# value to get more and more negative
				# 
				# there is no similar mirroring for left/right. low 
				# column numbers correspond to negative X values.
				p.x = 2 * (colIter - startCol)
				p.y = -4 if "LOW" in value.upper() else 0
				p.z = 2 * (startRow - rowIter)
				result.append( p )
				
				if value.upper().endswith( "GOAL" ):
					p2 = Placement( value )
					p2.partName = "goal"
					p2.x = 2 * (colIter - startCol)
					p2.y = -4 if "LOW" in value.upper() else 0
					p2.z = 2 * (startRow - rowIter)
					if "F2" in value.upper():
						# the 4x4 square should have a goal that is centered in it.
						# this is a special case, and a little hacky.
						p2.x += 1
						p2.z -= 1
					result.append( p2 )
				
	return result

def findStart( rows ):
	for rowIter, row in enumerate( rows ):
		for colIter, value in enumerate( row ):
			if value.upper().endswith( "-START" ):
				return rowIter, colIter
	raise Error( "Couldn't find start!" )


if __name__ == "__main__":
	main()			