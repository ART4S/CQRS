grammar QueryFiltering;

options
{
	language=CSharp;
}

query
    :   ('?'? queryParameter ('&' queryParameter)*)?
    ;

queryParameter
    :   top|skip|orderBy|select|filter
    ;

top
    :   '$top=' count=INT
    ;

skip
    :   '$skip='count=INT
    ;

orderBy
    :   '$orderBy=' expression=orderByExpression
    ;

orderByExpression
    :   orderByAtom[true] (',' orderByAtom[false])*
    ;

orderByAtom[bool isFirstSort]
    :   propertyName=PROPERTYACCESS sortType=(ASC|DESC)?
    ;

select
    :   '$select=' expression=selectExpression
    ;

selectExpression
    :   PROPERTYACCESS (',' PROPERTYACCESS)*
    ;

filter
    :   '$filter=' expression=filterExpression
    ;

filterExpression	
    :   filterAtom ((OR|AND) filterAtom)*
    ;

filterAtom
    :   not=NOT? (boolExpr=boolExpression | '(' filterExpr=filterExpression ')')
    ;

boolExpression
    :   left=atom 
        operation=(EQUALS|NOTEQUALS|GREATERTHAN|GREATERTHANOREQUAL|LESSTHAN|LESSTHANOREQUAL) 
        right=atom
    ;

atom
    :   property|constant|function
    ; 

property
    :   value=PROPERTYACCESS
    ;

constant
    :   value=(INT|LONG|DOUBLE|FLOAT|DECIMAL|BOOL|NULL|GUID|STRING|DATETIME)
    ;

function
    :   value=(TOUPPER|TOLOWER|STARTSWITH|ENDSWITH)'(' atom (',' atom)* ')'
    ;

OR
    :   'or'
    ;
AND
    : 	'and'
    ;

NOT
    :   'not'
    ;

EQUALS
    :   'eq'
    ;	

NOTEQUALS
    :   'ne'
    ;

GREATERTHAN
    :   'gt'
    ;

GREATERTHANOREQUAL
    :   'ge'
    ;	

LESSTHAN
    :   'lt'
    ;

LESSTHANOREQUAL
    :   'le'
    ;

ASC
    :   'asc'
    ;

DESC
    :   'desc'
    ;

TOUPPER
    :   'toupper'
    ;

TOLOWER
    :   'tolower'
    ;

STARTSWITH
    :   'startswith'
    ;
	
ENDSWITH
    :   'endswith'
    ;

INT
    :   '-'? NUMBER+
    ;

LONG
    :   '-'? NUMBER+ 'l'
    ;

DOUBLE
    :   '-'? NUMBER+ ('.' NUMBER+)? 'd'
    ;	

FLOAT
    :   '-'? NUMBER+ ('.' NUMBER+)? 'f'
    ;

DECIMAL
    :   '-'? NUMBER+ ('.' NUMBER+)? 'm'
    ;	

BOOL
    :   'true'|'false'
    ;

GUID
    :   HEX HEX HEX HEX HEX HEX HEX HEX '-' HEX HEX HEX HEX '-' HEX HEX HEX HEX '-' HEX HEX HEX HEX '-' HEX HEX HEX HEX HEX HEX HEX HEX HEX HEX HEX HEX
    ;

NULL
    :   'null'
    ;

DATETIME
    :	'datetime\'' NUMBER+ '-' NUMBER+ '-' NUMBER+ ('T' NUMBER+ ':' NUMBER+ (':' NUMBER+ ('.' NUMBER+)*)* ('Z')?)? '\''
    ;

STRING
    :   '\'' ( ESC | ~('\''|'\\') )* '\''
    ;

PROPERTYACCESS
    :   (PROPERTY ('.' PROPERTY)*)
    ;

WHITESPACE 
    :   [ \t\r\n]+ -> skip
    ;

fragment ESC
    :   '\\'(["\\/bfnrt]|UNICODE)
    ;

fragment UNICODE
    :   '\\' 'u' HEX HEX HEX HEX
    ;

fragment HEX
    :   [0-9a-fA-F] 
    ;

fragment PROPERTY
    :   LETTER (LETTER|NUMBER|'_')*
    ;

fragment NUMBER
    :   [0-9]
    ;

fragment LETTER
    :   [a-zA-Z]
    ;