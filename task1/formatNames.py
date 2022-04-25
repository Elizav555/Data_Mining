file1 = open("names.txt", "r")
f = open('namesRes.txt','w') 
lines = file1.readlines()
my_list = []
for line in lines:
        line = line.rstrip('\n')
        line = line.replace('@',"")
        if line.endswith('.'):
            line = line[0:len(line) - 1]
        if(line != 'kdukalis'):
            my_list.append(line)
my_list = map (lambda x: x + '\n', my_list) 
f.writelines(my_list)
file1.close
f.close()


   