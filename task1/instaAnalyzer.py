from time import sleep
from instabot import Bot
bot = Bot()
if bot.login(username = "unknown_dataminer", password = "12345678910datamining"):

    sleep(10)
    user_id = bot.get_user_id_from_username("kdukalis")
    sleep(10)
    user_info = bot.get_user_info(user_id) 
    sleep(10)
    print(user_info['biography'])

#user_followers = bot.get_user_followers('__elizav__') # Список подписчиков
#user_following = bot.get_user_following('__elizav__') # Список подписок
#print(user_followers)
#print(user_following)