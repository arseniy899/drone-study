from django.conf.urls import url
from django.urls import include
from rest_framework.routers import DefaultRouter
from questions.views import QuestionsViewSet

router = DefaultRouter()
router.register(r'questions', QuestionsViewSet, basename='questions')

urlpatterns = [
    url(r'^', include(router.urls)),
]
