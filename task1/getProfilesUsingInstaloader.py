import instaloader
L = instaloader.Instaloader()
file = open("nameRess.txt", "r")
while True:
     line = file.readline()
     if not line:
         break
     line =  line.rstrip()
     profile = instaloader.Profile.from_username(L.context, line)
     print(profile.full_name)

file.close