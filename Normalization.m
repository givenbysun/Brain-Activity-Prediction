Norm = zeros(1,25);
for i=1:60
    Norm(1,i)=norm(A(:,i),2);
end

for i=1:60
    if(Norm(1,i)==0)
        continue;
    end
    for j=1:25
        A(j,i)=A(j,i)/Norm(1,i);
    end

end
