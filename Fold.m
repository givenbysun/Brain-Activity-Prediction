RND(10,177)=0;
for i=1:177
    RND(1:10,i)=randperm(10);
end

TEN(1,1770)=0;

counter=0;
for i=1:10
    for j=1:177
        counter=counter+1;
        TEN(1,counter) = (j-1)*10 + RND(i,j);
    end
end
