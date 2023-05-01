import bs4
import requests
import pandas as pd


class Scraper:
    def __init__(self, url):
        self.url = url


    def get_soup(self):
        response = requests.get(self.url)
        soup = bs4.BeautifulSoup(response.text, 'html.parser')
        return soup


    def scrape(self)->pd.DataFrame:
        soup = self.get_soup()
        raise NotImplementedError
