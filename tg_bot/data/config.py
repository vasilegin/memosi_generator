from pathlib import Path
import os

BOT_TOKEN = ''
BASE_URL = ''

#take url from env if started via
API_URL = os.environ.get('API_URL') or "http://127.0.0.1:9999"
print(API_URL)
admins = []

ip = {
    'db':    '',
    'redis': '',
}

mysql_info = {
    'host':     ip['db'],
    'user':     '',
    'password': '',
    'db':       '',
    'maxsize':  5,
    'port':     3306,
}

redis = {
    'host':     ip['redis'],
    'password': ''
}

emoji = {
    '💩': -2,
    '😡': -1,
    '😐': 0,
    '🤣': 1,
    '🤡': 2,
}
