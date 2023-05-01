"""Scrape data from https://www.factmonster.com/math-science/inventions-discoveries/inventions-and-discoveries"""
from .scraper import Scraper

import pandas as pd
import regex as re

URL = "https://www.factmonster.com/math-science/inventions-discoveries/inventions-and-discoveries"


class ScrapeDiscoveries(Scraper):
    def __init__(self):
        super().__init__(url=URL)

    def scrape(self) -> pd.DataFrame:
        soup = self.get_soup()
        # only keep data in "Inventions and Discoveries" section
        soup = soup.find(id='mainaside')
        soup = str(list(soup.children)[2]).split("<dt")[1:-1]
        entries = []
        for soup_element in soup:
            data = soup_element.split(":</dt>")
            key, entry_list = data[0], data[1]
            key = parse_key(key)
            print(key)
            entry_list = entry_list.split(";")
            for entry in entry_list:
                year, subtitle = parse_entry(entry)
                entries.append([
                    key,
                    subtitle,
                    year
                ])

        df = pd.DataFrame(entries, columns=["title", "details", "year"])
        df['image'] = None
        df['source'] = URL
        df['year'] = df['year'].astype('Int64')
        df['id'] = df.index
        # reorder columns
        df = df[['id', 'title', 'details', 'year', 'image', 'source']]
        return df


def parse_key(input_text):
    """Extract substring between regex '>  </a>:' """
    input_text = input_text.strip("</a>")
    key = input_text.split(">")[-1]
    return key


def parse_entry(input_text):
    """Returns the year and subtitle"""

    year_pattern = re.compile(r"\d\d\d\d", re.IGNORECASE)
    year = year_pattern.findall(input_text)
    if year:
        year = int(year[-1])
    else:
        year = None

    if '(' in input_text:
        # get text in '(...)'
        pattern = re.compile(r"\((.*)\)", re.IGNORECASE)
        subtitle = pattern.findall(input_text)
        if subtitle:
            subtitle = subtitle[0]
            subtitle = subtitle.strip("()")
    else:
        subtitle = ""
    return year, subtitle


def test_entry_parser():
    input_text = '(absolute zero temperature, cessation of all molecular energy) <a href="/search/encyclopedia/Kelvin%2C+William+Thomson%2C+1st+Baron">William Thompson, Lord Kelvin</a>, England, 1219-1848.'
    year, subtitle = parse_entry(input_text)
    assert year == 1848
    assert subtitle == 'absolute zero temperature, cessation of all molecular energy'


def test_key_parser():
    input_text = '''id="A0903580"><a href="/search/encyclopedia/yellow+fever">Yellow Fever</a>'''
    key = parse_key(input_text)
    assert key == 'Yellow Fever'


discovery_scraper = ScrapeDiscoveries()
