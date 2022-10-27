import sys    
sys.path.append(sys.path[0]+'\..')

import asyncio
from pprint import pprint
from aiogram import Bot, Dispatcher, types, executor
from aiogram.types import ReplyKeyboardRemove, \
    ReplyKeyboardMarkup, KeyboardButton, \
    InlineKeyboardMarkup, InlineKeyboardButton

from common.emoji import emoji as emoji_map

with open('token.txt') as f:
    TOKEN = f.read()

bot = Bot(token=TOKEN)
dp = Dispatcher(bot)

emoji_markup = ReplyKeyboardMarkup(resize_keyboard = True).row(
    *[KeyboardButton(emoji[0]) for emoji in sorted(emoji_map.items(), key=lambda x: x[1])]
)

@dp.message_handler(commands=["start"])
async def cmd_start(message: types.Message):
    await message.reply("Ну привет, будем смотреть мемасы!", reply_markup=emoji_markup)

@dp.message_handler(lambda message: message.text in emoji_map)
async def emoji(message: types.Message):
    await message.reply('ага, понял')



if __name__ == "__main__":
    executor.start_polling(dp, skip_updates=True)
