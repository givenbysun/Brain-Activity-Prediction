function [ dist ] = PearsonCorrelation(V1,V2) 
if ( isequal( size(V1) , size(V2) ) ) 
    M = corr([V1 ; V2]') ;
    dist = M(1,2); %% or M(2,1)
else 
    dist = -1; 
end
